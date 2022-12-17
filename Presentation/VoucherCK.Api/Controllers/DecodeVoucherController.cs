using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoucherCK.Api.Requests;
using VoucherCK.Application.Commands.DeCodeVoucherCommands;

namespace VoucherCK.Api.Controllers
{
    [Route("voucher")]
    [ApiController]
    public class DecodeVoucherController : BaseController
    {
        private readonly IMediator _mediator;

        public DecodeVoucherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("decode")]
        public async Task<object> DecodeVoucherAsync([FromBody] DecodeRequest request)
        {
            var response = await _mediator.Send(new DecodeVoucherCommand(request.BarCode));
            return Ok(response);
        }
    }
}
