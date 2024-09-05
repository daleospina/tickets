using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cicloso.tickets.core.Facade;
using cicloso.tickets.entities;

namespace cicloso.tickets.portal.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                CustomerFacade customerFacade = new CustomerFacade();
                CustomerList customers = customerFacade.GetList();

                return View(customers);

            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                CustomerFacade customerFacade = new CustomerFacade();
                customerFacade.Add(customer);

                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                CustomerFacade customerFacade = new CustomerFacade();
                Customer customer = new Customer();

                customer = customerFacade.Get(id);
                return View(customer);
            }
            catch (Exception exc)
            {
                return View();
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Customer customer)
        {
            try
            {
                CustomerFacade customerFacade = new CustomerFacade();
                customerFacade.Update(customer);   

                return RedirectToAction("Index");
            }
            catch(Exception exc)
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                CustomerFacade customerFacade = new CustomerFacade();
                Customer customer = new Customer();
                customer = customerFacade.Get(id);

                return View(customer);
            }
            catch (Exception exc)
            {
                return View();
            }
        }


        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Customer customer)
        {
            try
            {
                CustomerFacade customerFacade = new CustomerFacade();
                customerFacade.Delete(id);
             
                return RedirectToAction("Index");
            }
            catch (Exception exc)
            {
                return View();
            }
        }
    }
}
