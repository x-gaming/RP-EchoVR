namespace RP_EchoVR {
    class EchoData {
        public string match_type { get; set; } = "loading...";
        public string map_name { get; set; } = "loading...";
        public string client_name { get; set; } = "loading...";
        public string game_status { get; set; } = "loading...";
        public int blue_points { get; set; } = 0;
        public int orange_points { get; set; } = 0;
        public float game_clock { get; set; } = 0.0f;
        public bool private_match { get; set; } = false;
        public LastScore last_score { get; set; }
        public Team[] teams { get; set; }
    }
    class LastScore {
        public string team { get; set; }
        public int point_amount { get; set; }
        public string goal_type { get; set; }
    }

    class Team {
        public string team { get; set; }
        public Player[] players { get; set; }
    }

    class Player {
        public string name { get; set; }
        public int level { get; set; }
    }
}
