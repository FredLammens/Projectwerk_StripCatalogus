﻿using DataLayer.DataBaseClasses;
using DataLayer.Extension_Methods;
using DomainLibrary.DomainLayer;
using DomainLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        #region Properties
        /// <summary>
        /// Connection with the datebase.
        /// </summary>
        private AdoNetContext context;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to make a ComicRepository.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public OrderRepository(AdoNetContext context)
        {
            this.context = context;
        }
        #endregion

        #region AddDomainObject
        public void AddOrder(Order order)
        {
            DOrder toAdd = Mapper.ToDOrder(order);
            SetComicIds(toAdd);
            AddDOrder(toAdd);
            LinkStockToOrder(toAdd);
        }

        #endregion

        #region AddDataObject
        private void AddDOrder(DOrder dOrder)
        {
            using (var command = context.CreateCommand())
            {
                command.CommandText = $@"Insert into Orders (OrderDate) " +
                                        "values (@date);";
                command.AddParameter("date", dOrder.Date);
                dOrder.Id = (int)command.ExecuteScalar();
            }
        }

        private void LinkStockToOrder(DOrder dOrder)
        {
            using (var command = context.CreateCommand())
            {
                command.AddParameter("order_Id", dOrder.Id);
                int i = 0;
                foreach (var comicPair in dOrder.OrderComics)
                {
                    command.CommandText = @"Select Stock.ID From Stock " +
                                          $"where Stock.ComicID = @comic_Id{i};";

                    command.AddParameter($"Comic_Id{i}", comicPair.Key.Id);

                    int StockID = (int)command.ExecuteScalar();

                    command.CommandText = @"insert into OrdersComics (OrderID, StockID, AmountOrdered) " +
                                          $"values (@order_Id, @stock_Id{i}, @amount{i});";

                    command.AddParameter($"stock_Id{i}", StockID);
                    command.AddParameter($"amount{i}", comicPair.Value);

                    command.CommandText = @"UPDATE Stock " +
                                          $"SET Stock.Stock += @amount{i}" +
                                          $"WHERE Stock.ID = @stock_Id{i};";

                    i++;
                }

            }
        }

        #endregion

        #region GetIds
        private void SetComicIds(DOrder toAdd)
        {
            using (var command = context.CreateCommand())
            {

                int i = 0;
                foreach (var comic in toAdd.OrderComics.Keys)
                {

                    command.CommandText = @$"Select * From Series Where Series.name = @name{i}";
                    command.AddParameter($"name{i}", comic.Series.Name);
                    int? seriesId = (int?)command.ExecuteScalar();

                    if (seriesId == null)
                        throw new DataException($"Series {comic.Series.Name} is not in the database");

                    command.CommandText = @$"Select * From Comics Where Comics.Title = @title{i}  AND Comics.SeriesNr = @series_Nr{i} AND Comics.Series_ID = @series_Id{i};";
                    command.AddParameter($"title{i}", comic.Title);
                    command.AddParameter($"series_Nr{i}", comic.SeriesNumber);
                    command.AddParameter($"series_Id{i}", seriesId);
                    int? id = (int?)command.ExecuteScalar();

                    if (id == null)
                        throw new DataException($"Comic {comic.Title} is not in the database");

                    comic.Id = (int)id;

                    i++;
                }
            }
        }

        #endregion



    }
}
