﻿using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace RP_EchoVR {
    class DataFetcher {
        private readonly string url;
        public DataFetcher(string url) {
            this.url = url;
        }
        public EchoData GetData() {
            WebClient client = new WebClient();
            EchoData data = new EchoData();
            try {
                Stream stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);
                data = JsonSerializer.Deserialize<EchoData>(reader.ReadToEnd());
            } catch (Exception) { }

            return data;
        }
    }
}
