using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineCampaign.Enums;
using OnlineCampaign.Models;
//using OnlineCampaign.Models.Dtoos;
using OnlineCampaign.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;

namespace OnlineCampaign.Controllers
{
    [ApiController]
    [Route("api/onlineCampaign/option")]
    public class OptionsController : ControllerBase
    {
        //[HttpPost]
        //public async Task<ActionResult<Option>> CreateData(CreateOptionDto input)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            var id = 0;
        //            if (context.Options.ToList().Count == 0)
        //            {
        //                id = 1;
        //            }
        //            else
        //            {
        //                id = context.Options.OrderByDescending(x => x.OptionId).FirstOrDefault().OptionId + 1;
        //            }

        //            context.Options.Add(new Option()
        //            {
        //                OptionId = id,
        //                QuestionId = input.QuestionId,
        //                QuestionType = input.QuestionType,
        //                OptionName = input.OptionName,
        //                OptionValue = input.OptionValue
        //            });

        //            context.SaveChanges();

        //            var response = new OptionResponseDto()
        //            {
        //                OptionId = id,
        //                QuestionId = input.QuestionId,
        //                QuestionType = input.QuestionType,
        //                OptionName = input.OptionName,
        //                OptionValue = input.OptionValue
        //            };
        //            return StatusCode(StatusCodes.Status200OK, response);
        //        }

        //        catch (Exception ex)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }
        //    }
        //}

        //[HttpGet]
        //public async Task<ActionResult<List<Option>>> GetListDataAsync()
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            IList<OptionResponseDto> optionList = new List<OptionResponseDto>();
        //            optionList = context.Options.ToList()
        //                         .Select(x => new OptionResponseDto()
        //                         {
        //                             OptionId = x.OptionId,
        //                             QuestionId = x.QuestionId,
        //                             QuestionType = x.QuestionType,
        //                             OptionName = x.OptionName,
        //                             OptionValue = x.OptionValue
        //                         }).ToList<OptionResponseDto>();
        //            return StatusCode(StatusCodes.Status200OK, optionList);
        //        }
        //        catch (Exception ex)
        //        {

        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }

        //    }
        //}

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<ActionResult> GetDatabyOptionId(int id)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            var options = context.Options.Where(x => x.OptionId == id)
        //                                  .Select(x => new OptionResponseDto()
        //                                  {
        //                                      OptionId = x.OptionId,
        //                                      QuestionId = x.QuestionId,
        //                                      QuestionType = x.QuestionType,
        //                                      OptionName = x.OptionName,
        //                                      OptionValue = x.OptionValue

        //                                  }).FirstOrDefault();
        //            return StatusCode(StatusCodes.Status200OK, options);
        //        }
        //        catch (Exception ex)
        //        {

        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }
        //    }
        //}

        //[HttpPut]
        //public async Task<ActionResult<UpdateOptionDto>> UpdateData(int id, [FromBody] UpdateOptionDto input)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            var existingOption = context.Options.Where(x => x.OptionId == id).FirstOrDefault();

        //            if (existingOption != null)
        //            {
        //                if (input.QuestionType == existingOption.QuestionType)
        //                {
        //                    existingOption.QuestionType = input.QuestionType;
        //                }

        //                if (input.OptionName == existingOption.OptionName)
        //                {
        //                    existingOption.OptionName = input.OptionName;
        //                }
        //                if (input.OptionValue == existingOption.OptionValue)
        //                {
        //                    existingOption.OptionValue = input.OptionValue;
        //                }
        //                context.Options.Update(existingOption);
        //                context.SaveChanges();
        //                return StatusCode(StatusCodes.Status200OK, existingOption);
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }
        //    }

        //}

        //[HttpDelete]
        //[Route("{id}")]
        //public async Task<ActionResult> DeleteData(int id)
        //{
        //    using (var context = new OnlineCampaignContext())
        //    {
        //        try
        //        {
        //            var existingOption = context.Options.Where(x => x.OptionId == id)
        //                             .FirstOrDefault();
        //            if (existingOption != null)
        //            {
        //                context.Options.Remove(existingOption);
        //                context.SaveChanges();
        //                return StatusCode(StatusCodes.Status200OK, "Record Successfully Deleted");

        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status404NotFound, "No Record Found For This Id");
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //        }
        //    }

        //}


        // Relations on Both Tables Options && Question

        [HttpPost]
        [Route("createlist")]

        public async Task<ActionResult<List<Option>>> CreateRelationData([FromBody]List<Option> input)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {

                    foreach (var data in input)
                    {
                        context.Options.Add(new Option()
                        {
                            OptionId = data.OptionId,
                            QuestionId = data.QuestionId,
                            QuestionType = data.QuestionType,
                            OptionName = data.OptionName,
                            OptionValue = data.OptionValue,

                            Question = new Question()
                            {
                                Id = data.Question.Id,
                                Name = data.Question.Name,
                                QuestionType = data.Question.QuestionType,
                                IsPublished = data.Question.IsPublished,
                            }

                        });
                        context.SaveChanges();


                    }
                    return Ok(input);
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Option>> GetRelationDatabyOptionId(int id)
        {
            using(var context = new OnlineCampaignContext())
            {
                try
                {
                    var dataByOptionId = (from option in context.Options.Where(option => option.OptionId == id).ToList()
                                  join question in context.Questions
                                  on option.QuestionId equals question.Id
                                  select new
                                  {
                                      OptionId = option.OptionId,
                                      QuestionId = option.QuestionId,
                                      QuestionType = option.QuestionType,
                                      OptionName = option.OptionName,
                                      OptionValue = option.OptionValue,
                                      Question = new Question()
                                      {
                                          Id = option.Question.Id,
                                          Name = option.Question.Name,
                                          QuestionType = option.Question.QuestionType,
                                          IsPublished = option.Question.IsPublished
                                      }

                                  }).ToList();
                    return Ok(dataByOptionId);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

                }

            }
        }

        [HttpGet]
        [Route("get/all_list")]
        public async Task<ActionResult<Option>> GetRelationDatabyAllList()
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var dataByAllList = (from option in context.Options.ToList()
                                         join question in context.Questions
                                         on option.QuestionId equals question.Id
                                         select new
                                         {
                                             OptionId = option.OptionId,
                                             QuestionId = option.QuestionId,
                                             QuestionType = option.QuestionType,
                                             OptionName = option.OptionName,
                                             OptionValue = option.OptionValue,
                                             Question = new Question()
                                             {
                                                 Id = option.Question.Id,
                                                 Name = option.Question.Name,
                                                 QuestionType = option.Question.QuestionType,
                                                 IsPublished = option.Question.IsPublished

                                             }

                                         }).ToList();

                    return Ok(dataByAllList);
                }
                catch (Exception ex )
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }


            }
        }

        [HttpPut]
        [Route("update/data")]

        public async Task<ActionResult<Option>> UpdateData(int id, Option input)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
                    var existingOption = context.Options.Where(option => option.OptionId == id).FirstOrDefault();
                    if (existingOption != null)
                    {
                        var updateRecord = context.Questions.FirstOrDefault(question => question.Id == existingOption.Question.Id);
                        if (updateRecord != null)
                        {
                            updateRecord.Name = input.Question.Name;
                            updateRecord.QuestionType = input.Question.QuestionType;
                            updateRecord.IsPublished = input.Question.IsPublished;

                            var questionUpdate = context.Questions.Update(updateRecord);

                            existingOption.QuestionId = input.QuestionId;
                            existingOption.QuestionType = input.QuestionType;
                            existingOption.OptionName = input.OptionName;
                            existingOption.OptionValue = input.OptionValue;

                            var optionUpdate = context.Options.Update(existingOption);

                            return StatusCode(StatusCodes.Status200OK);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status204NoContent, "No Content Found");
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound,"No Data Found");
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
        public async Task<ActionResult> DeleteRelationData(int id)
        {
            using (var context = new OnlineCampaignContext())
            {
                try
                {
    
                    var existingOption = (from option in context.Options
                                         join question in context.Questions
                                         on option.QuestionId equals question.Id
                                         where option.QuestionId == id
                                         select option).ToList();
                    if (existingOption != null)
                    {
                        context.Options.RemoveRange(existingOption);
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
