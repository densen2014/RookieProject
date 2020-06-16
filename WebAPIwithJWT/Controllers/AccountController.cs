using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIwithJWT.Controllers
{

    //[Authorize]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        ///// <summary>
        ///// 创建信息
        ///// </summary>
        ///// <param name="createViewModel">参数</param>
        ///// <returns>状态</returns>
        //[HttpPost]
        //public StatusViewModel Post([FromBody] CreateViewModel createViewModel)
        //{
        //    return new StatusViewModel { };
        //}

        ///// <summary>
        ///// 删除信息
        ///// </summary>
        ///// <param name="deleteViewModel">参数</param>
        ///// <returns></returns>
        //[HttpDelete]
        //public StatusViewModel Delete([FromQuery] DeleteViewModel deleteViewModel)
        //{
        //    return new StatusViewModel { };
        //}

        ///// <summary>
        ///// 查询信息
        ///// </summary>
        ///// <param name="queryViewModel">参数</param>
        ///// <returns></returns>
        //[HttpGet]
        //public StatusViewModel Get([FromQuery] QueryViewModel queryViewModel)
        //{
        //    return new StatusViewModel { };
        //}

        ///// <summary>
        ///// 修改信息
        ///// </summary>
        ///// <param name="updateViewModel">参数</param>
        ///// <returns></returns>
        //[HttpPut]
        //public StatusViewModel Put([FromQuery] UpdateViewModel updateViewModel)
        //{
        //    return new StatusViewModel { };
        //}
    }
}
