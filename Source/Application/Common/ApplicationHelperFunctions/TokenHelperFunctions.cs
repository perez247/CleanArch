using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Domain.UserSection;

namespace Application.Common.ApplicationHelperFunctions
{
    /// <summary>
    /// Token helper functions static class
    /// </summary>
    public static class TokenHelperFunctions
    {
        /// <summary>
        /// Generate the token for user within the application
        /// </summary>
        /// <param name="userTokenData"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public static string generateUserToken(User userTokenData, bool rememberMe = false)
        {

            // Create token an sent;
            var claims = defaultClaim(userTokenData);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(EnvironemtUtilityFunctions.AUTHORIZATION_TOKEN));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = rememberMe ? RememberMeDescriptor(claims, creds) : defaultDescriptor(claims, creds);

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            // var data = new {token = tokenhandler.WriteToken(token)};

            return tokenhandler.WriteToken(token);
        }

        /// <summary>
        /// Default token with a short timeframe because no remember me was enabled
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        public static SecurityTokenDescriptor defaultDescriptor(List<Claim> claims, SigningCredentials creds)
        {
            // Set date of nbf to now
            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                NotBefore = DateTime.Now.AddMinutes(-5),
                IssuedAt = DateTime.Now,
                SigningCredentials = creds
            };
        }

        /// <summary>
        /// Longer timeframe token because remember me was enabled
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        public static SecurityTokenDescriptor RememberMeDescriptor(List<Claim> claims, SigningCredentials creds)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                NotBefore = DateTime.Now.AddMinutes(-5),
                IssuedAt = DateTime.Now,
                SigningCredentials = creds
            };
        }

        /// <summary>
        /// Get the user of the instance/request
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public static string GetUserId(ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        private static List<Claim> defaultClaim(User userTokenData)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userTokenData.Id.ToString()),
                new Claim(AppClaimTypes.userType.ToString(), userTokenData.Discriminator.ToString().ToLower()),
                // new Claim(AppClaimTypes.profilePicture.ToString(), userTokenData.ProfilePicture),
                // new Claim(AppClaimTypes.emojiPicture.ToString(), userTokenData.EmojiPicture)
            };



            foreach (var item in userTokenData.UserRoles)
            {
                // claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));
            }

            return claims;
        }
    }

    /// <summary>
    /// Custom claim types
    /// </summary>
    public enum AppClaimTypes
    {

        /// Type of user
        userType,

        /// Roles of user
        userRoles,

        /// Profile Picture
        profilePicture,

        /// Emoji Picture
        emojiPicture
    }
}