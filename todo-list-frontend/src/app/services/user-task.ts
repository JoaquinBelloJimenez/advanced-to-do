import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserTask } from '../models/user-task';

export type CreateTaskDto = Omit<TaskService, 'id' | 'createdAt' | 'updatedAt'>;

@Injectable({
  providedIn: 'root',
})

export class TaskService {
private apiUrl = 'https://localhost:5001/api/tasks'; // Ajusta el puerto si es necesario

  constructor(private http: HttpClient) { }

  getAllTasks(): Observable<UserTask[]> {
    return this.http.get<UserTask[]>(this.apiUrl);
  }

  getTaskById(id: number): Observable<UserTask> {
    return this.http.get<UserTask>(`${this.apiUrl}/${id}`);
  }

  createTask(task: UserTask): Observable<UserTask> {
    return this.http.post<UserTask>(this.apiUrl, task);
  }

  updateTask(id: number, task: UserTask): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, task);
  }

  deleteTask(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}