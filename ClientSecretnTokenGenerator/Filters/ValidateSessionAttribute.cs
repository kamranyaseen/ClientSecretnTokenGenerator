﻿using System;
using System.Web.Mvc;

namespace ClientSecretnTokenGenerator.Filters
{
    public class ValidateSessionAttribute : FilterAttribute,IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])))
            {
                ViewResult result = new ViewResult();
                result.ViewName = "Error";
                result.ViewBag.ErrorMessage = "Cannot Access Pages without Login";
                filterContext.Result = result;
            }
        }
    }
}