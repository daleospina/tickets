using cicloso.tickets.core.Facade;
using cicloso.tickets.entities;
using cicloso.tickets.portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cicloso.tickets.portal.Controllers
{
    public class TheaterController : Controller
    {
        // GET: Theater
        public ActionResult Index()
        {
            try
            {
                TheaterFacade theaterFacade = new TheaterFacade();
                TheaterList theaters = theaterFacade.GetList();

                return View(theaters);
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: Theater/Details/5
        public ActionResult Details(int id)

        {
            return View();
        }

        // GET: Theater/Create
        public ActionResult Create()
        {
            try
            {
                TheaterModel model = new TheaterModel();
                CityList cities = new CityList();
                               
                CityFacade cityFacade = new CityFacade();
                cities = cityFacade.GetList();

                model.Cities = cities.Cities.Select(list => new SelectListItem { Value = list.Id, Text = list.Name });
                return View(model);
            }
            catch (Exception exc)
            {
                return View();
            }

        }

        // POST: Theater/Create
        [HttpPost]
        public ActionResult Create(TheaterModel model)
        {
            try
            {
                TheaterFacade theaterFacade = new TheaterFacade();
                Theater theater = new Theater();
                theater.Id = model.Id;
                theater.IdCity = model.IdCity;
                theater.Name = model.Name;
                theater.Address = model.Address;
                theater.State = model.State.ToString();

                theaterFacade.Add(theater);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }

        }

        // GET: Theater/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                TheaterFacade theaterFacade = new TheaterFacade();
                Theater theater = new Theater();

                theater = theaterFacade.Get(id);
                return View(theater);
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // POST: Theater/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Theater theater)
        {
            try
            {
                TheaterFacade theaterFacade = new TheaterFacade();
                theaterFacade.Update(theater);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: Theater/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                TheaterFacade theaterFacade = new TheaterFacade();
                Theater theater = new Theater();

                theater = theaterFacade.Get(id);
                return View(theater);
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // POST: Theater/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Theater theater)
        {
            try
            {
                TheaterFacade theaterFacade = new TheaterFacade();
                theaterFacade.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }
    }
}




