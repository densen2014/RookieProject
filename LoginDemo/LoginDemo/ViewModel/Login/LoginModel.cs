namespace LoginDemo.ViewModel.Login
{
    /// <summary>
    /// 登录窗口Model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 数据填写正确
        /// </summary>
        public bool IsValid { get; set; }
    }
}
