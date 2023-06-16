using System.Security.Claims;

namespace Insurance.Service.Base
{
	public static class IdentityExtension
	{
		public static string GetUsername(this ClaimsPrincipal user)
		{
			return user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name)?.Value;
		}

		public static long GetUserId(this ClaimsPrincipal user)
		{
			return Convert.ToInt64(user.Claims.FirstOrDefault(i => i.Type == ClaimTypes.UserData)?.Value);
		}
	}
}