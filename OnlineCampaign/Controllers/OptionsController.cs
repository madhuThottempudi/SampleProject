using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineCampaign.Enums;
using OnlineCampaign.Models;
using OnlineCampaign.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCampaign.Controllers
{
    [ApiController]
    [Route("api/onlineCampaign/option")]
    public class OptionsController : ControllerBase
    {
        [HttpPost]
        public async  Task<ActionResult<Option>> CreateData(CreateOptionDto input )
        {
            using(var context = new OnlineCampaignContext())
            {
                try
                {
                    var id = 0;
                    if(context.Options.ToList().Count == 0)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = context.Options.OrderByDescending(x => x.OptionId).FirstOrDefault().OptionId + 1;
                    }

                    context.Options.Add(new Option()
                    {
                        OptionId = id,
                        QuestionId = input.QuestionId,
                        QuestionType = input.QuestionType,
                        OptionName = input.OptionName,
                        OptionValue = input.OptionValue
                    });
                    context.SaveChanges();

                    var response = new OptionResponseDto()
                    {
                        OptionId = id,
                        QuestionId = input.QuestionId,
                        QuestionType = input.QuestionType,
                        OptionName = input.OptionName,
                        OptionValue = input.OptionValue
                    };
                    return StatusCode(StatusCodes.Status200OK, response);
                }

                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Option>>> GetListDataAsync()
        {
            using(var context = new OnlineCampaignContext())
            {
                try
                {
                    IList<OptionResponseDto> optionList = new List<OptionResponseDto>();
                    optionList = context.Options.ToList()
                                 .Select(x => new OptionResponseDto()
                                 {
                                     OptionId = x.OptionId,
                                     QuestionId = x.QuestionId,
                                     QuestionType = x.QuestionType,
                                     OptionName = x.OptionName,
                                     OptionValue = x.OptionValue
                                 }).ToList<OptionResponseDto>();
                    return StatusCode(StatusCodes.Status200OK, optionList);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }

            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetDatabyOptionId(int id)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var options = context.Options.Where(x => x.OptionId == id)
                                          .Select(x => new OptionResponseDto()
                                          {
                                              OptionId = x.OptionId,
                                              QuestionId = x.QuestionId,
                                              QuestionType = x.QuestionType,
                                              OptionName = x.OptionName,
                                              OptionValue = x.OptionValue

                                          }).FirstOrDefault();
                    return StatusCode(StatusCodes.Status200OK, options);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult<UpdateOptionDto>> UpdateData(int id, [FromBody] UpdateOptionDto input)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var existingOption = context.Options.Where(x => x.OptionId == id).FirstOrDefault();

                    if (existingOption != null)
                    {
                        if (input.QuestionType == existingOption.QuestionType)
                        {
                            existingOption.QuestionType = input.QuestionType;
                        }

                        if (input.OptionName == existingOption.OptionName)
                        {
                            existingOption.OptionName = input.OptionName;
                        }
                        if (input.OptionValue == existingOption.OptionValue)
                        {
                            existingOption.OptionValue = input.OptionValue;
                        }
                        context.Options.Update(existingOption);
                        context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, existingOption);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
                    }
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteData(int id)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var existingOption = context.Options.Where(x => x.OptionId == id)
                                     .FirstOrDefault();
                    if (existingOption != null)
                    {
                        context.Options.Remove(existingOption);
                        context.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK, "Record Successfully Deleted");

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
                    }
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        }


    }
}
