
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataLayer;
using Website.Models;

namespace Website.Controllers
{
    [CookiesExpireFilterCommon]
    public class AngularApiController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/qualifiationList")]
        public QualifiationMasterList QualifiationList(int pageSize, int pageIndex, int Id = 0)
        {

            StudentAdmissionQualification obj = new StudentAdmissionQualification();
            QualifiationMasterList sub = new QualifiationMasterList();
            sub = obj.QualificationdetailList(pageIndex, pageSize);
            for (int i = 0; i < sub.qlist.Count; i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/collageallocationList")]
        public CollegeCourseAllocationList collageallocationList(int pageSize, int pageIndex, int Id = 0)
        {

            CollegeCourseAllocation obj = new CollegeCourseAllocation();

            CollegeCourseAllocationList sub = new CollegeCourseAllocationList();
            sub = obj.collageallocationdetailList(pageIndex, pageSize);
            return sub;

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/documentList")]
        public DocumentUploadList DocumentList(int pageSize, int pageIndex, int Id = 0)
        {

            DocumentUpload obj = new DocumentUpload();

            DocumentUploadList sub = new DocumentUploadList();
            sub = obj.DocumentdetailList(pageIndex, pageSize);
            for(int i=0;i<sub.qlist.Count;i++)
            {
                sub.qlist[i].EncriptedID = EncriptDecript.Encrypt(sub.qlist[i].ID.ToString());
            }
            return sub;

        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/collageList")]
        public CollageList collageList(int pageSize, int pageIndex, int Id = 0)
        {

            BL_CollegeMaster obj = new BL_CollegeMaster();

            CollageList sub = new CollageList();
            sub = obj.collagedetailList(pageIndex, pageSize);
            return sub;

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/get-student-list")]
        public BL_StudentList StudentList(int pageIndex1, int pageSize1, string ApplicationNo, string session, int IsFeeSubmit = 0, string name = "", int CourseCategory = 0, int Subject = 0, int CollegeID = 0)
        {
            BL_StudentList obj = new BL_StudentList();
            BL_StudentList objStudentList = new BL_StudentList();
            objStudentList = obj.StudentList(pageIndex1, pageSize1, ApplicationNo, session, IsFeeSubmit, name, CourseCategory, Subject, CollegeID );
            return objStudentList;

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/get-seat-list")]
        public Bl_SeatList SeatList(int pageSize, int pageIndex, int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0")
        {
            Bl_SeatMater obj = new Bl_SeatMater();
            Bl_SeatList sub = new Bl_SeatList();
            sub = obj.GetseatList("Couserseatview", pageIndex, pageSize, Id, courseid, programid, sessionid, admissionid);
            return sub;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/get-collegeseat-list")]
        public Bl_SeatList SeatListcollgeg(int pageSize, int pageIndex, int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0")
        {
            Bl_SeatMater obj = new Bl_SeatMater();
            Bl_SeatList sub = new Bl_SeatList();
            sub = obj.GetseatListCollege("Couserseatview", pageIndex, pageSize, Id, courseid, programid, sessionid, admissionid);
            return sub;
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/get-admissiondates-list")]
        public Bl_SeatList admissiondates(int pageSize, int pageIndex, int Id = 0, string courseid = "0", string programid = "0", string sessionid = "0", string admissionid = "0")
        {
            Bl_SeatMater obj = new Bl_SeatMater();
            Bl_SeatList sub = new Bl_SeatList();
            sub = obj.Getadmissiondateslist("Couserseatview", pageIndex, pageSize, Id, courseid, programid, sessionid, admissionid);
            return sub;
        }
        //--bharati 01/04/2019
        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/getSubjectList")]
        public SubjectList getSubjectList(int pageSize, int pageIndex, string search = "", string searchcode = "", string searchname = "", string practical = "", string compulsory = "")
        {
            BL_StreamMaster obj = new BL_StreamMaster();
            SubjectList objsub = new SubjectList();

            objsub = obj.GetSubjectListing(pageIndex, pageSize, search, searchcode, searchname, practical, compulsory);
            return objsub;

        }
        
    }
}
