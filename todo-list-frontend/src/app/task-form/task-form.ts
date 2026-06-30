// src/app/task-form/task-form.component.ts
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { TaskService, CreateTaskDto } from '../services/user-task';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './task-form.html',
  styleUrls: ['./task-form.css']
})
export class TaskFormComponent {
  // Usa CreateTaskDto en lugar de Omit<Task, ...>
  task: CreateTaskDto = {
    title: '',
    description: '',
    isCompleted: false
  };

  private taskService = inject(TaskService);
  private router = inject(Router);

  onSubmit(): void {
    this.taskService.createTask(this.task).subscribe(() => {
      this.router.navigate(['/']);
    });
  }
}