using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Linq;
using NHibernate.Linq;

namespace Sandbox.Tests
{
	[TestClass]
	public class User
	{
		private static ISessionFactory CreateSessionFactory()
		{
			var dbFile = "Sandbox.Tests.db";
			return Fluently.Configure()
				.Database(
					SQLiteConfiguration.Standard.UsingFile(dbFile).ShowSql()
				)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Sandbox.Core.User>())
				.ExposeConfiguration(config =>
				{
					if (File.Exists(dbFile))
					{
						File.Delete(dbFile);
					}

					new SchemaExport(config).Create(false, true);
				})
				.BuildSessionFactory();
		}

		private ISessionFactory _sessionFactory;

		[TestInitialize]
		public void TestInitialize()
		{
			_sessionFactory = CreateSessionFactory();

			using (var session = _sessionFactory.OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var thzinc = new Core.User()
				{
					Username = "thzinc",
					DisplayName = "Daniel James"
				};

				var greenbabe17 = new Core.User()
				{
					Username = "greenbabe17",
					DisplayName = "Bonnie James"
				};

				thzinc.Data.Add(new Core.Datum()
				{
					Descriptor = "weight",
					User = thzinc,
					Value = "391.6"
				});

				thzinc.Data.Add(new Core.Datum()
				{
					Descriptor = "age",
					User = thzinc,
					Value = "29"
				});

				thzinc.Data.Add(new Core.Datum()
				{
					Descriptor = "color.favorite",
					User = thzinc,
					Value = "blue"
				});

				Assert.AreEqual(3, thzinc.Data.Count);

				greenbabe17.Data.Add(new Core.Datum()
				{
					Descriptor = "age",
					User = greenbabe17,
					Value = "32"
				});

				greenbabe17.Data.Add(new Core.Datum()
				{
					Descriptor = "color.favorite",
					User = greenbabe17,
					Value = "green"
				});

				Assert.AreEqual(2, greenbabe17.Data.Count);

				session.SaveOrUpdate(thzinc);
				session.SaveOrUpdate(greenbabe17);

				transaction.Commit();
			}
		}

		[TestCleanup]
		public void TestCleanup()
		{
			_sessionFactory.Close();
		}

		[TestMethod]
		public void EnsureTestDataIsSane()
		{
			using (var session = _sessionFactory.OpenSession())
			{
				var users = session.Query<Core.User>()
					.ToList();

				Assert.IsNotNull(users);
				Assert.AreEqual(2, users.Count);
				foreach (var user in users)
				{
					switch (user.Username)
					{
						case "thzinc":
							Assert.AreEqual(3, user.Data.Count);
							Assert.AreEqual("blue", user.Data.FirstOrDefault(d => d.Descriptor == "color.favorite").Value);
							break;
						case "greenbabe17":
							Assert.AreEqual(2, user.Data.Count);
							Assert.AreEqual("green", user.Data.FirstOrDefault(d => d.Descriptor == "color.favorite").Value);
							break;
						default:
							Assert.Fail();
							break;
					}
				}

				Console.WriteLine(new string('-', 80));

				var data = session.Query<Core.Datum>()
					.ToList();

				Assert.IsNotNull(data);
				Assert.AreEqual(5, data.Count);
				foreach (var datum in data)
				{
					Console.WriteLine("({0}) {1}: {2}", datum.User.Username, datum.Descriptor, datum.Value);
				}
			}
		}
	}
}
