

namespace securityAPI
{
    public static class authToken
    {
        private static string userAccessString { get; set; }
        private static int userID { get; set; }
        private static string userName { get; set; }
        private static string userFirstName { get; set; }


        /// <summary>
        /// Create a userAccessString in the authToken to show that a user has been verified by the login system
        /// </summary>
        /// <param name="userID">The ID of the user collected by the login system</param>
        public static void AuthorizeUser(int AuthorizedUserID, string AuthorizedUserName, string AuthorizedUserFirstName)
        {
            userID = AuthorizedUserID;
            userName = AuthorizedUserName;
            userFirstName = AuthorizedUserFirstName;
            userAccessString = "1111";
        }

        public static void DeauthorizeCurrentUser()
        {
            userID = 0;
            userName = null;
            userFirstName = null;
            userAccessString = null;
        }

        public static string GetFirstName()
        {
            return userFirstName;
        } 

        public static bool IsUserAuthorized()
        {          
            return userAccessString != null;
        }

    }
}
