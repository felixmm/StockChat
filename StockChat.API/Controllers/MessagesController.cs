﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockChat.API.Helpers;
using StockChat.API.Models;

namespace StockChat.API.Controllers
{
    [Route("Messages")]
    public class MessagesController : Controller
    {
        private readonly MessagesRepository repo;

        public MessagesController()
        {
            repo = new MessagesRepository();
        }

        [Route("")]
        [HttpGet]
        public List<Message> GetLastMessages()
        {
            var messages = repo.GetLastMessages();
            return messages;
        }

        [Route("")]
        [HttpPost]
        public ActionResult SendMessage([FromBody]Message message)
        {
            this.repo.Save(message);
            return Ok(message);
        }
    }
}