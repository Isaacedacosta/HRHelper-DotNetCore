import { Component, OnInit } from '@angular/core';
import { Local } from 'protractor/built/driverProviders';
import { UserDataService } from '../_data-services/user.data-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: any[] = [];
  user: any = {};
  userLogin: any = {};
  userLogged: any = {};
  showList: boolean = true;
  isAuthenticated: boolean = false;

  constructor(private userDataService: UserDataService) { }

  ngOnInit() {
    
  }

  get() {
    this.userDataService.get().subscribe((data: any[]) => {
      this.users = data;
      this.user = {};
      this.showList = true;
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  save() {
    if (this.user.id) {
      this.put();
    } else {
      this.post();
    }
  }

  
  post() {
    this.userDataService.post(this.user).subscribe(data => {
      if (data) {
        alert('User added to DB');
        this.user = {};
        this.logOff();
      } else {
        alert('Error');
      }
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  put() {
    this.userDataService.put(this.user).subscribe(data => {
      if (data) {
        alert('User updated to DB');
        this.user = {};
        this.get();
      } else {
        alert('Error');
      }
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  showUserDetails(userDetail) {
    this.showList = false;
    this.user = userDetail;
  }

  deleteUser(user) {
    this.userDataService.delete().subscribe(data => {
      if (data) {
        alert('User deleted from DB');
        this.user = {};
        this.userLogged = {};
        this.isAuthenticated = false;
        this.get();
      } else {
        alert('Error');
      }
    }, error => {
      alert('System Error');
    })
  }


  authenticate() {
    this.userDataService.authenticate(this.userLogin).subscribe(data => {
      localStorage.setItem('loginData', JSON.stringify(data));
      this.setUserLogged();
      this.get();
    }, error => {
      console.log(error);
      alert('System Error');
    })
  }

  setUserLogged() {
    this.userLogged = JSON.parse(localStorage.getItem('loginData'));
    this.isAuthenticated = (this.userLogged != null);
  }

  logOff() {
    this.users = [];
    this.user= {};
    this.userLogin = {};
    this.userLogged= {};
    this.showList = true;
    this.isAuthenticated = false;
    localStorage.removeItem('loginData');
  }


}
