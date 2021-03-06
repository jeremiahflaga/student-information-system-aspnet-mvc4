﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudInfoSys.Models;
using StudInfoSys.Repository;
using StudInfoSys.ViewModels;

namespace StudInfoSys.Controllers
{
    [Authorize]
    public class CandidateStudentController : Controller
    {
        private readonly CandidateStudentRepository _studentRepository;

        public CandidateStudentController(CandidateStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        //
        // GET: /Student/

        public ActionResult Index()
        {
            return View(_studentRepository.GetAll().OrderBy(s => new { s.LastName, s.FirstName }).ToList());
        }

        //
        // GET: /Student/Details/5

        public ActionResult Details(int id = 0)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            return View(new StudentViewModel());
        }

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = MapStudentViewModelToStudent(studentViewModel);
                _studentRepository.Insert(student);
                _studentRepository.Save();
                return RedirectToAction("Index");
            }

            return View(studentViewModel);
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            var studViewModel = MapStudentToStudentViewModel(student);
            return View(studViewModel);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = MapStudentViewModelToStudent(studentViewModel);
                _studentRepository.Update(student);
                _studentRepository.Save();
                return RedirectToAction("Index");
            }
            return View(studentViewModel);
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            _studentRepository.Delete(student);
            _studentRepository.Save();
            return RedirectToAction("Index");
        }
        
        #region Private methods

        private static StudentViewModel MapStudentToStudentViewModel(Student student)
        {
            var studViewModel = new StudentViewModel
            {
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                FirstName = student.FirstName,
                Gender = student.Gender,
                Id = student.Id,
                LastName = student.LastName,
                Photo = student.Photo,
                StudentStatus = student.StudentStatus,
                Email = student.Email
            };
            return studViewModel;
        }

        private static Student MapStudentViewModelToStudent(StudentViewModel studentViewModel)
        {
            var studViewModel = new Student
            {
                Address = studentViewModel.Address,
                DateOfBirth = studentViewModel.DateOfBirth,
                FirstName = studentViewModel.FirstName,
                Gender = studentViewModel.Gender,
                Id = studentViewModel.Id,
                LastName = studentViewModel.LastName,
                Photo = studentViewModel.Photo,
                StudentStatus = studentViewModel.StudentStatus,
                Email = studentViewModel.Email
            };
            return studViewModel;
        }

        #endregion

    }
}