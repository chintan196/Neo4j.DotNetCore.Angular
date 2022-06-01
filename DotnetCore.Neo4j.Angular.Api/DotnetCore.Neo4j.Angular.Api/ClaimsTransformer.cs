using System.Security.Claims;

namespace DotnetCore.Neo4j.Angular.Api
{
    // This class can be used to implement the AD based authorization. Not in use for now.
    /// <summary>
    /// Class ClaimsTransformer.
    /// Implements the <see cref="IClaimsTransformation" />
    /// </summary>
    /// <seealso cref="IClaimsTransformation" />
    public class ClaimsTransformer
    {
        /// <summary>
        /// Provides a central transformation point to change the specified principal.
        /// Note: this will be run on each AuthenticateAsync call, so its safer to
        /// return a new ClaimsPrincipal if your transformation is not idempotent.
        /// </summary>
        /// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> to transform.</param>
        /// <returns>The transformed principal.</returns>
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            //var wi = (WindowsIdentity)principal.Identity;

            //if (wi.Groups != null)
            //{
            //    foreach (var group in wi.Groups)
            //    {
            //        try
            //        {
            //            var check = group.Translate(typeof(NTAccount)).ToString();
            //            // var claim = new Claim(wi.RoleClaimType, group.Value);
            //            var claim = new Claim(wi.RoleClaimType, check);
            //            wi.AddClaim(claim);
            //        }
            //        catch (Exception ex)
            //        {
            //            throw ex;
            //        }
            //    }

            //    var staticClaim = new Claim("Role", "Admin");
            //    wi.AddClaim(staticClaim);

            //    staticClaim = new Claim("Role", "Regular");
            //    wi.AddClaim(staticClaim);
            //}
            return Task.FromResult(principal);
        }
    }
}
