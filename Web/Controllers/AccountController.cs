﻿using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Account;
using Web.ViewModels.Manage;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ITokenClaimService _tokenClaimService;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
            //,ITokenClaimService tokenClaimService
            )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            //this._tokenClaimService = tokenClaimService;
        }

        [Authorize(Roles = "Super Admin,Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model)
        {
            //string ReturnUrl = "";
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Login Attempt..");
                    return View(model);
                }
                var isValidUserAndPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (isValidUserAndPassword == false)
                {
                    ModelState.AddModelError("", "Invalid Login Attempt..");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                //var jwtToken =await _tokenClaimService.GetTokenAsync(model.UserName);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResestPassword ()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ResestPassword(ChangePasswordVM model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                    if(isPasswordCorrect)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                        if(result.Succeeded)
                        {
                            TempData["SuccessMessage"] = "Password Changed Successfully..";
                            return View(model);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Something Went Wrong";
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Or Old Password Not Correct..Please Check");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Or Old Password Not Correct..Please Check");
                }
            }
            return View(model);
        }
    }
}
