import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ToDoService } from '../to-do-service.service';

@Component({
  selector: 'app-to-do',
  templateUrl: './to-do.component.html',
  styleUrls: ['./to-do.component.sass']
})
export class ToDoComponent implements OnInit {
    
    public tasklist : any[] = [];
    form !: FormGroup;
    id : number | undefined;
    action = "Add Task";
  
    constructor(private fb : FormBuilder, private service : ToDoService, private toastr : ToastrService) 
    {
        
        this.form = this.fb.group
        ({
            taskName: [''],
            description: [''],
            dueDate: [''],
            priority: ['']
        });
    }

  ngOnInit(): void {
    this.getAllTasks();
  }

  getAllTasks()
  {
    this.service.getTasks().subscribe
    (
      {
        next: (Data: any) =>
        {
            this.tasklist = Data;
            console.log(Data);
        },
        error: (Error : any) => console.log(Error)
      }
    )
  }

  addTask(){
    const Task : any = 
    {
        taskName : this.form.get('taskName')?.value,
        description : this.form.get('description')?.value,
        dueDate : this.form.get('dueDate')?.value,
        isDone : false,
        priority : isNaN(parseInt(this.form.get('priority')?.value)) ? 1 : parseInt(this.form.get('priority')?.value)
    };

    if (this.id == undefined) {
        
        this.service.createTask(Task).subscribe({
                next: () =>{
                    this.toastr.success('You have succesfully added a task!', 'Congratulations!');
                    this.getAllTasks(); 
                    this.form.reset();
                },
                error: (Data:any) => {
                    this.toastr.error('You added an invalid data.', 'Something went wrong');
                    console.log(Data);
                }
            }
        )
    } else {

        Task.taskID = this.id;
        this.service.updateTask(this.id, Task).subscribe({
                next: () => {
                    this.form.reset();
                    this.action = "Add Task";
                    this.id = undefined;
                    this.toastr.info('The task was updated succesfully', 'Task upddated!');
                    this.getAllTasks();
                },
                error : (data:any) => {
                    console.log(data);
                }
            }
        )
    }
    
  }

  deleteTask(id : any){
    this.service.deleteTask(id).subscribe
    (
        {
            next: () =>
            {
                this.toastr.error('You have deleted the task succesfully!', 'Task deleted');
                this.getAllTasks();
            },
            error: (Data:any) =>
            {
                console.log(Data);
            }
        }
    )
  }

  editTask(data:any){
   this.action = "Edit Task";
   this.id = data.taskID;

   this.form.patchValue({
    taskName: data.taskName,
    description: data.description,
    dueDate: data.dueDate,
    isDone: true,
    priority: data.priority
   })
    
  }
}
