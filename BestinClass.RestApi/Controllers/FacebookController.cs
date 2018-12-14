using Microsoft.AspNetCore.Mvc;

namespace BestinClass.RestApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FacebookController : Controller
  {
    /*[HttpPost]
public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
{
// 1.generate an app access token
var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
// 2. validate the user access token
  var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
  var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

if (!userAccessTokenValidation.Data.IsValid)
  {
    return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
  }

// 3. we've got a valid token so we can request user data from fb
var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

// 4. ready to create the local user account (if necessary) and jwt
var user = await _userManager.FindByEmailAsync(userInfo.Email);

if (user == null)
{
   var appUser = new AppUser
   {
     FirstName = userInfo.FirstName,
     LastName = userInfo.LastName,
     FacebookId = userInfo.Id,
     Email = userInfo.Email,
     UserName = userInfo.Email,
     PictureUrl = userInfo.Picture.Data.Url
   };

   var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

   if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

   await _appDbContext.Customers.AddAsync(new Customer { IdentityId = appUser.Id, Location = "",Locale = userInfo.Locale,Gender = userInfo.Gender});
   await _appDbContext.SaveChangesAsync();
  }

// generate the jwt for the local user...
var localUser = await _userManager.FindByNameAsync(userInfo.Email);

if (localUser==null)
{
   return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
}

var jwt = await Tokens.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id), _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings {Formatting = Formatting.Indented});

return new OkObjectResult(jwt);
}
*/
  }
}