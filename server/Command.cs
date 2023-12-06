﻿namespace server {
    internal class Command {
        public const string ProccessList = "PROCLIST";
        public const string Kill = "KILL";
        public const string Run = "RUN";
        public string? Text { get; set; }
        public string? Param { get; set; }
    }
}
