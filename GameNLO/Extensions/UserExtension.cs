namespace GameNLO.Extensions
{
    public static class UserExtension
    {
        private const string CookieName = "UserId";

        public static Guid SetUserId(this HttpContext context)
        {
            var userId = Guid.NewGuid();
            context.Response.Cookies.Append(CookieName, userId.ToString());
            return userId;
        }

        public static Guid? GetUserId(this HttpContext context)
        {
            var userId = context.Request.Cookies[CookieName];
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return Guid.Parse(userId);
        }

        public static Guid GetOrCreateUserId(this HttpContext context)
        {
            var userId = GetUserId(context);
            if (userId.HasValue)
            {
                return userId.Value;
            }
            else
            {
                return SetUserId(context);
            }
        }
    }
}
