namespace Kapta.API.Controllers
{
    using System;
    using System.IO;
    using System.Web.Http;
    using Common.Models;
    using Helpers;
    using Newtonsoft.Json.Linq;

    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        //mandamos todos los campos, que estan en userRequest
        public IHttpActionResult PostUser(UserRequest userRequest)
        {
            //validamos si hay foto para guardarla
            if (userRequest.ImageArray != null && userRequest.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(userRequest.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                //la guardamos en content/users
                var folder = "~/Content/Users";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    userRequest.ImagePath = fullPath;
                }
            }

            var answer = UsersHelper.CreateUserASP(userRequest);
            if (answer.IsSuccess)
            {
                return Ok(answer);
            }
            return BadRequest(answer.Message);
        }

        [HttpPost]
        [Authorize]
        [Route("GetUser")]
        public IHttpActionResult GetUser(JObject form)
        {
            try
            {
                var email = string.Empty;
                dynamic jsonObject = form;

                try
                {
                    email = jsonObject.Email.Value;
                }
                catch
                {
                    return BadRequest("Incorrect call.");
                }

                var user = UsersHelper.GetUserASP(email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /*
        [HttpPost]
        [Route("LoginFacebook")]
        public IHttpActionResult LoginFacebook(FacebookResponse profile)
        {
            var user = UsersHelper.GetUserASP(profile.Id);
            if (user != null)
            {
                return Ok(true);
            }

            var userRequest = new UserRequest
            {
                EMail = profile.Id,
                FirstName = profile.FirstName,
                ImagePath = profile.Picture.Data.Url,
                LastName = profile.LastName,
                Password = profile.Id,
            };

            var answer = UsersHelper.CreateUserASP(userRequest);
            return Ok(answer);
        }

        [HttpPost]
        [Route("LoginTwitter")]
        public IHttpActionResult LoginTwitter(TwitterResponse profile)
        {
            var user = UsersHelper.GetUserASP(profile.IdStr);
            if (user != null)
            {
                return Ok(true); // TODO: Pending update the user with new twitter data
            }

            var firstName = string.Empty;
            var lastName = string.Empty;
            var fullName = profile.Name;
            var posSpace = fullName.IndexOf(' ');
            if (posSpace == -1)
            {
                firstName = fullName;
                lastName = fullName;
            }
            else
            {
                firstName = fullName.Substring(0, posSpace);
                lastName = fullName.Substring(posSpace + 1);
            }

            var userRequest = new UserRequest
            {
                EMail = profile.IdStr,
                FirstName = firstName,
                ImagePath = profile.ProfileImageUrl,
                LastName = lastName,
                Password = profile.IdStr,
            };

            var answer = UsersHelper.CreateUserASP(userRequest);
            return Ok(answer);
        }
        */


    }
}
