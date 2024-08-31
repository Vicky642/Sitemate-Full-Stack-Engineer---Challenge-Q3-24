using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitemate.Models;
using Sitemate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitemate.Controllers
{
    [Route("Sitemate")]
    [ApiController]
    public class SitemateController : ControllerBase
    {
        private readonly SitemateRepository<Issue> _issueRepository;

        public SitemateController(SitemateRepository<Issue> issueRepository)
        {
            _issueRepository = issueRepository;
        }

        // GET: Sitemate
        [HttpGet]
        public ActionResult<IEnumerable<Issue>> GetIssues()
        {
            var issues = _issueRepository.GetAll(); // Assumes GetAll method exists
            return Ok(issues);
        }

        // GET: Sitemate/5
        [HttpGet("{id}")]
        public ActionResult<Issue> GetIssue(int id)
        {
            var issue = _issueRepository.Get(id);

            if (issue == null)
            {
                return NotFound();
            }

            return Ok(issue);
        }

        // POST: Sitemate
        [HttpPost]
        public ActionResult<Issue> PostIssue(Issue issue)
        {
            _issueRepository.Add(issue);
            return CreatedAtAction(nameof(GetIssue), new { id = issue.Id }, issue);
        }

        // PUT: Sitemate/5
        [HttpPut("{id}")]
        public IActionResult PutIssue(int id, Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest();
            }

            _issueRepository.Update(issue);
            return NoContent();
        }

        // DELETE: Sitemate/5
        [HttpDelete("{id}")]
        public IActionResult DeleteIssue(int id)
        {
            var existingIssue = _issueRepository.Get(id);

            if (existingIssue == null)
            {
                return NotFound();
            }

            _issueRepository.Delete(id);
            return NoContent();
        }

    }
}
