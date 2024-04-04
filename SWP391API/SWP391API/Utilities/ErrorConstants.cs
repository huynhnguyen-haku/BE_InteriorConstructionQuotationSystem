namespace SWP391API.Utilities
{
    public static class ErrorConstants
    {
        public static string UserNotFound = "User not found";
        public static string UserNotActive = "User is not active";
        public static string RoleNotFound = "Role not found";
        public static string UsernameAlreadyExists = "Username already exists";
        public static string EmailAlreadyExists = "Email already exists";
        public static string UserAlreadyActive = "User is already active";
        public static string InvalidToken = "Invalid token";
        public static string ExpiredToken = "Expired token";
        public static string QuotationNotFound = "Quotation not found";
        public static string ProjectNotFound = "Project not found";
        public static string CategoryNotFound = "Category not found";
        public static string ProductNotFound = "Product not found";
        public static string ProductExistInQuotationDetail = "Product exist in quotation detail";
        public static string ProductExistInProject = "Product exist in project";
        public static string ProductExistInQuotationTemps = "Product exist in quotation temps";
        public static string QuotationNotPending = "Quotation is not pending status";
    }
}
