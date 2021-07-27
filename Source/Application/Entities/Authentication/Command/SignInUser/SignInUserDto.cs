using System;
using Domain.UserSection;

namespace Application.Entities.Authentication.Command.SignInUser
{
    /// <summary>
    /// What will be returned back to the user
    /// </summary>
    public class SignInUserDto
    {
        /// <summary>
        /// The Jwt token for authorization
        /// </summary>
        /// <value></value>
        public string Token { get; set; }
        
        /// <summary>
        /// User
        /// </summary>
        /// <value></value>
        public SignedInUserDto User { get; set; }

        /// <summary>
        /// Generate the dto for signed in user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static SignInUserDto Generate(User user, string token)
        {
            var data = new SignInUserDto();

            data.Token = token;

            data.User = SignedInUserDto.Generate(user);

            return data;
        }
    }

    /// <summary>
    /// User part of the data
    /// </summary>
    public class SignedInUserDto {
        /// <summary>
        /// Id of the User
        /// </summary>
        /// <value></value>
        public string Id { get; set; }

        /// <summary>
        /// Full name of the owner
        /// </summary>
        /// <value></value>
        public string FullName { get; set; }

        /// <summary>
        /// Full name of the owner
        /// </summary>
        /// <value></value>
        public string UserName { get; set; }


        /// <summary>
        /// Profile picture of the entity
        /// </summary>
        /// <value></value>
        public string ProfilePicture { get; set; }

        /// <summary>
        /// Type of this user
        /// </summary>
        /// <value></value>
        public string UserType { get; set; }

        /// <summary>
        /// Email of this user
        /// </summary>
        /// <value></value>
        public string Email { get; set; }

        // /// <summary>
        // /// Local address of the entity
        // /// </summary>
        // /// <value></value>
        // public EntityAddressDto LocalAddress { get; set; }

        // /// <summary>
        // /// Place the individual works
        // /// </summary>
        // /// <value></value>
        // public PlaceOfWorkDto PlaceOfWork { get; set; }

        /// <summary>
        /// Id of the head quarter if its an organization
        /// </summary>
        /// <value></value>
        public string HeadQuarterId { get; set; }

        /// <summary>
        /// If its dark mode or not
        /// </summary>
        /// <value></value>
        public bool DarkMode { get; set; }

        /// <summary>
        /// Display localized ads
        /// </summary>
        /// <value></value>
        public bool DisplayLocalizedAds { get; set; }

        /// <summary>
        /// If user is new
        /// </summary>
        /// <value></value>
        public bool NewUser { get; set; }

        /// <summary>
        /// Create an instance of SignedInUserDto with required data from user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static SignedInUserDto Generate(User user) {
            var data = new SignedInUserDto();

            data.Id = user.Id.ToString();

            data.FullName = user.GetFullName();

            data.UserName = user.UserName;

            data.Email = user.Email;

            // data.UserType = user.Discriminator.ToLower();

            // data.HeadQuarterId = user.AdditionalDetail.
            // data.localAddress = user

            data.DisplayLocalizedAds = user.AdditionalDetail.DisplayLocalAds;
            
            data.DarkMode = user.AdditionalDetail.DarkMode;

            data.NewUser = user.AdditionalDetail.NewUser;
            return data;
        }
    }
}