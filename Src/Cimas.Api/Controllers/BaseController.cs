﻿using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Cimas.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public BaseController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

        protected IActionResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Failure => StatusCodes.Status400BadRequest,
                ErrorType.Validation => StatusCodes.Status422UnprocessableEntity,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, detail: error.Description);
        }

        protected IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }

        protected IActionResult NoContent(Success success)
            => NoContent();
    }
}
