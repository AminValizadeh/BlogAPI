using System.IO;

namespace Blog.Core.Utils.Common
{
    public static class PathTools
    {
        #region domain

        public static string Domain = "https://localhost:44381";

        #endregion

        #region product

        public static string ProductImagePath = "/images/products/origin/";
        public static string ProductImageServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products/origin/");

        #endregion


    }
}
