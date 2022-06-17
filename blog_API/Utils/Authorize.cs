namespace blog_API.Utils {
    public class Authorize {

        public static bool HasPermissionAdmin(string hash) {

            return BCrypt.Net.BCrypt.Verify("admin", hash);
        }

    }
}
