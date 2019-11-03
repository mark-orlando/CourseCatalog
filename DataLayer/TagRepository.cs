﻿using DataLayer.Entities;
using DataLayerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CourseCatalog.api.Services {
    public class TagRepository : ITagRepository {

        private readonly IConfiguration configuration;
        private readonly string conn = string.Empty;

        public TagRepository(IConfiguration config) {
            configuration = config;
            conn = config.GetConnectionString("DefaultConnection");
        }

        public string Get() {
            string jsonOutput = string.Empty;
            string spName = "jsonGetTags";

            try {
                using (SqlConnection cn = new SqlConnection(conn)) {
                    var cmd = new SqlCommand(cmdText: spName, connection: cn) {
                        CommandType = CommandType.StoredProcedure
                    };

                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows) {
                        reader.Read();
                        jsonOutput = reader["JsonOutput"].ToString();
                    }
                    reader.Close();
                }
                return jsonOutput;
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }

        public void Update(Tag tag) {
            string jsonInput = JsonConvert.SerializeObject(tag, Formatting.None);
            string spName = "jsonUpdateTag";

            try {
                using (SqlConnection cn = new SqlConnection(conn)) {
                    var cmd = new SqlCommand(cmdText: spName, connection: cn) {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue(parameterName: "@JsonInput", value: jsonInput);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
