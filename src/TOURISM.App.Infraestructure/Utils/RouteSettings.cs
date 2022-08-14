using System.Collections.Generic;

namespace TOURISM.App.Infrastructure.Utils
{
    public class RouteSettings
    {
        public List<AreaSettings> Areas { get; set; }
    }

    public class AreaSettings
    {
        public string Name { get; set; }
        public List<ControllerSettings> Controllers { get; set; }
    }

    public class ControllerSettings
    {
        public string Name { get; set; }
        public List<ActionSettings> Actions { get; set; }
    }

    public class ActionSettings
    {
        public string Name { get; set; }
        public string KeyContent { get; set; }
    }
}
