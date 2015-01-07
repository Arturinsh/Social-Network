using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_WEB_Page.Models
{
    public class HomeDefaultPageOutput
    {
      public   class NotAcceptedFriend
        {          
            public int Id { get; set; }
            public string UserCredits { get; set; }
            public string userImg { get; set; } 
            public string IdUser { get; set; }       
            public DateTime date { get; set; }        
            public string IdFriend { get; set; }      
            public int Accepted { get; set; }


           public void InsertData(int Id, string IdUser, DateTime date,string IdFriend,int Accepted)
          {
            this.Id = Id;
            this.IdUser = IdUser;
            this.date = date;
            this.IdFriend = IdFriend;
            this.Accepted = Accepted;

        }//<-- insert user
        }//<-- class end
        public ApplicationUser LoggedInUser { get; set; }
        public List<NotAcceptedFriend> NotAcceptedFriends = new List<NotAcceptedFriend>();
        public List<Announcments> annoucments = new List<Announcments>();
        //<-- My newwest friends
        public List<ApplicationUser> friendsMyStart = new List<ApplicationUser>();
        public void Insert(int _Id, string _IdUser, DateTime _date, string _IdFriend, int _Accepted, String _UserCredits, String _UserImg)
        {
            

            NotAcceptedFriends.Add(new NotAcceptedFriend{Id=_Id, IdUser=_IdUser, date=_date,IdFriend=_IdFriend,Accepted=_Accepted, UserCredits=_UserCredits, userImg=_UserImg });
        }
    }//<-- class end
}//<-- namespace ned