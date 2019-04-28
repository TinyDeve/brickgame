using System.Collections.Generic;

namespace UTNotifications
{
    public sealed class JsonUtils
    {
        public static JSONArray ToJson(ICollection<Button> buttons)
        {
            if (buttons == null || buttons.Count == 0)
            {
                return null;
            }

            var json = new JSONArray();

            foreach (var it in buttons)
            {
                var button = new JSONClass();
                button.Add("title", it.title);

                var userData = ToJson(it.userData);
                if (userData != null)
                {
                    button.Add("userData", userData);
                }

                json.Add(button);
            }

            return json;
        }

        public static JSONNode ToJson(IDictionary<string, string> userData)
        {
            if (userData == null || userData.Count == 0)
            {
                return null;
            }

            var json = new JSONClass();
            foreach (var it in userData)
            {
                json.Add(it.Key, new JSONData(it.Value));
            }

            return json;
        }
    }
}