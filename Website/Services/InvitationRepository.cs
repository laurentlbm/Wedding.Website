using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;
using Website.Models;
using Dapper;

namespace Website.Services
{
    internal class InvitationRepository
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public Invitation Get(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Get<Invitation>(id);
            }
        }

        public IEnumerable<Invitation> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.GetList<Invitation>(sort: new[] { new Sort { PropertyName = "FullName", Ascending = true } });
            }
        }

        public void MarkAsSent(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("update Invitations set InvitationSentAt = @Now where Id = @Id", new { Id = id, Now = DateTimeOffset.UtcNow });
            }
        }

        public void MarkAsViewed(Guid id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Execute("update Invitations set LastViewedAt = @Now where Id = @Id", new { Id = id, Now = DateTimeOffset.UtcNow });
            }
        }

        public void Update(Invitation invitation)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                connection.Update(invitation);
            }
        }

        public Guid Create(Invitation invitation)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                return connection.Insert(invitation);
            }
        }
    }
}