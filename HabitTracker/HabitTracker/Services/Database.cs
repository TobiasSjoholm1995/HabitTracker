using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using HabitTracker.Models;
using HabitTracker.Converters;

namespace HabitTracker.Services
{
    static class Database
    {
        public const string Extension = ".habit";

        public static async Task Save(string filename, IEnumerable<Habit> tasks)
        {
            if (tasks == null)
                return;

            var builder = new StringBuilder();

            foreach (var task in tasks)
            {
                var line = HabitConverter.Convert(task);

                if (!string.IsNullOrWhiteSpace(line))
                    builder.Append(line);
            }

            var filepath = GetFilepath(filename);

            using (StreamWriter sw = new StreamWriter(filepath, false))
                await sw.WriteAsync(builder.ToString());
        }

        public static List<Habit> Read(string filename)
        {
            var habits = new List<Habit>();

            if (filename == null)
                return habits;

            var path = GetFilepath(filename);

            if(!File.Exists(path))
                return habits;

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var task = HabitConverter.Convert(line);

                if (task != null)
                    habits.Add(task);
            }

            return habits;
        }

        private static string GetFilepath(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return string.Empty;

            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string name = "HabitDatabase";
            var folderPath = Path.Combine(folder, name);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return Path.Combine(folderPath, filename);
        }

    }

}
