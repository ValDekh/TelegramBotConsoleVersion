﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotConsoleVersion.Services
{
    internal class MessageInfo
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string MessageContainer_id { get; set; }
        public int Message_id { get; set; }
        public string Message_text { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
    }
}
