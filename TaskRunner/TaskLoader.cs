﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TaskRunner
{
    internal class TaskLoader : ITaskLoader
    {
         public List<TaskInformation> FindTasks()
         {
             return (from t in Assembly.GetEntryAssembly().GetTypes()
                     where typeof (ITask).IsAssignableFrom(t)
                     select new TaskInformation(t, GetDescription(t))).ToList();
         }

         private TaskDescriptionAttribute GetDescription(Type type)
         {
             var description = type.GetCustomAttributes(true).Where(a => a is TaskDescriptionAttribute).FirstOrDefault() as TaskDescriptionAttribute;
             return description == null ? new TaskDescriptionAttribute(string.Empty, string.Empty) : description;
         }
    }
}