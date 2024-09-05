using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cicloso.tickets.core.Facade;
using cicloso.tickets.entities;

namespace cicloso.tickets.portal.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            try
            {
                CityFacade cityFacade = new CityFacade();
                CityList cities = cityFacade.GetList();

                return View(cities);

            }
            catch (Exception exc)
            {
                return View();
            }

        }

        // GET: City/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: City/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: City/Create
        [HttpPost]
        public ActionResult Create(City city)
        {
            try
            {
                CityFacade cityFacade = new CityFacade();
                cityFacade.Add(city);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: City/Edit/5
        public ActionResult Edit(string id)

        {
            try
            {
                CityFacade cityFacade = new CityFacade();
                City city = new City();

                city = cityFacade.Get(id);
                return View(city);
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // POST: City/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, City city)
        {
            try
            {
                CityFacade cityFacade = new CityFacade();
                cityFacade.Update(city);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: City/Delete/5
        public ActionResult Delete(string id)
        { 
            try
            {
                CityFacade cityFacade = new CityFacade();
                City city = new City();
                city = cityFacade.Get(id);

                return View(city);
            }
            catch (Exception exc)
            {
                return View();
            }

        }

        // POST: City/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Customer customer)
        {
            try
            {
                CityFacade cityFacade = new CityFacade();
                cityFacade.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }
    }
}
