import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToDoService {

    private app_url : string = "https://localhost:7179/";
    private api_url : string = "api/tasks/";

  constructor(private http:HttpClient) { }

  getTasks() : Observable<any>
  {
    return this.http.get(this.app_url + this.api_url);
  }

  getTasksByPriority() : Observable<any>
  {
    return this.http.get(this.app_url + this.api_url + "by_priority");
  }

  getSingleTask(id : any) : Observable<any>
  {
    return this.http.get(this.app_url + this.api_url + id);
  }

  createTask(data : any) : Observable<any>
  {
    return this.http.post(this.app_url + this.api_url, data);
  }

  updateTask(id : any, data : any) : Observable<any>
  {
    return this.http.put(this.app_url + this.api_url + id, data);
  }

  deleteTask(id : any) : Observable<any>
  {
    return this.http.delete(this.app_url + this.api_url + id);
  }
}
