﻿using System.Web;
using System.Web.Mvc;
using asp_mv_2.Models.DB;
using Asp_mvc_2.Models.EntityManager;
namespace Asp_mvc_2.Security
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        private readonly string[] userAssignedRoles;
        public AuthorizeRolesAttribute(params string[] roles)
        {
            this.userAssignedRoles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            using (DEMODB2Entities db = new DEMODB2Entities())
            {
                UserManager UM = new UserManager();
                foreach (var roles in userAssignedRoles)     
                {
                    authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
                    if (authorize)
                        return authorize;
                }
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
        }
    }
}