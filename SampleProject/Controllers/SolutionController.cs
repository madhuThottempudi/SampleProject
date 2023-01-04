using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SampleProject.Enums;
using SampleProject.Migrations;
using SampleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SampleProject.Controllers
{
    [ApiController]
    [Route("api/solution")]
    public class SolutionController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<Question>>> InsertingData([FromBody]List<Question> question)
        {
            using (var que = new SolutionContext())
            {
                try
                {
                    foreach (var item in question)
                    {
                        que.Questions.Add(new Question()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            QuestionType = item.QuestionType,
                            IsPublished = item.IsPublished,
                            QuestionRefId = item.QuestionRefId,

                            Option = new Option()
                            {
                                QuestionId = item.Option.QuestionId,
                                QuestionType = item.Option.QuestionType,
                                OptionName = item.Option.OptionName,
                                OptionValue = item.Option.OptionValue,
                                QuestionRefId = item.Option.QuestionRefId,

                                Answer = new Answer()
                                {
                                    QuestionId = item.Option.Answer.QuestionId,
                                    OptionsId = item.Option.Answer.OptionsId,
                                    AnswerValue = item.Option.Answer.AnswerValue
                                }
                            }
                        });

                        que.SaveChanges();

                    }
                    return StatusCode(StatusCodes.Status200OK);
                }
                catch (Exception ex)
                {
                    return StatusCode (StatusCodes.Status500InternalServerError, "No Data Found");
                }

            }
         

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Question>> GetData(int id)
        {
            using (var que = new SolutionContext())
            {
                try
                {
                    var Result = (from q in que.Questions.Where(q => q.Id == id).ToList()
                                  join o in que.Options on q.QuestionRefId equals o.QuestionId
                                  join a in que.Answers on o.Answer.QuestionId equals a.QuestionId
                                  select new
                                  {
                                      Id = q.Id,
                                      Name = q.Name,
                                      QuestionType = q.QuestionType,
                                      OptionName = o.OptionName,
                                      OptionValue = o.OptionValue,
                                      AnswerValue = a.AnswerValue

                                  }).ToList();
                   return StatusCode(StatusCodes.Status200OK);
                    //return Ok(Result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "No Data Found");
                }

            }

        }

        [HttpPut]
        [Route("update/list")]
        public async Task<ActionResult<Question>> DataUpdation(int id,Question question)
        {
            using (var que = new SolutionContext())
            {
                try
                {
                    var existingData = que.Questions.Where(q => q.Id == id).FirstOrDefault();
                    if (existingData != null)
                    {
                        var updateData = que.Options.Where(o => o.QuestionId == existingData.Option.QuestionRefId).FirstOrDefault();
                        if (updateData != null)
                        {
                            updateData.OptionName = question.Option.OptionName;
                            updateData.OptionValue = question.Option.OptionValue;

                            var optionUpdate = que.Options.Update(updateData);

                            existingData.Name = question.Name;
                            existingData.IsPublished = question.IsPublished;

                            var questionUpdate = que.Questions.Update(existingData);

                            que.SaveChanges();

                            return StatusCode(StatusCodes.Status200OK);


                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status204NoContent, "no record have found");
                        }


                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "NOt Inserted Properly");
                    }

                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, "Properly no data Found");
                }


            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Question>> Deletedata(int id)
        {
            using (var solution = new SolutionContext())
            {
                //var solu = solution.Questions.Where(c => c.Id == id).FirstOrDefault();

                try
                {
                    var solu = from q in solution.Questions
                               join o in solution.Options on q.QuestionRefId equals o.QuestionId

                               where q.Id == id
                               select o;


                    solution.Options.RemoveRange(solu);
                    solution.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {

                    return StatusCode(StatusCodes.Status500InternalServerError, " no data Found");
                }
            }
         
        }

    }
} 
