import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { PageHeaderModule } from '@delon/abc/page-header';

interface ItemData {
  id: number;
  name: string;
  age: number;
  address: string;
}
@Component({
  selector: 'app-do-show-table',
  standalone: true,
  imports: [CommonModule, NzTableModule, FormsModule, NzInputModule, PageHeaderModule],
  templateUrl: './do-show-table.component.html',
  styleUrls: ['./do-show-table.component.less']
})
export class DoShowTableComponent implements OnInit {

  searchText: string = '';
  originalListOfData: readonly ItemData[] = [];
  listOfSelection = [
    {
      text: 'Select All Row',
      onSelect: () => {
        this.onAllChecked(true);
      }
    },
    {
      text: 'Select Odd Row',
      onSelect: () => {
        this.listOfCurrentPageData.forEach((data, index) => this.updateCheckedSet(data.id, index % 2 !== 0));
        this.refreshCheckedStatus();
      }
    },
    {
      text: 'Select Even Row',
      onSelect: () => {
        this.listOfCurrentPageData.forEach((data, index) => this.updateCheckedSet(data.id, index % 2 === 0));
        this.refreshCheckedStatus();
      }
    }
  ];
  checked = false;
  indeterminate = false;
  listOfCurrentPageData: readonly ItemData[] = [];
  listOfData: readonly ItemData[] = [];
  setOfCheckedId = new Set<number>();

  updateCheckedSet(id: number, checked: boolean): void {
    if (checked) {
      this.setOfCheckedId.add(id);
    } else {
      this.setOfCheckedId.delete(id);
    }
  }

  onItemChecked(id: number, checked: boolean): void {
    this.setOfCheckedId.clear(); // Limpia todas las selecciones previas
    if (checked) {
      this.setOfCheckedId.add(id);
    }
    this.refreshCheckedStatus();
  }

  onAllChecked(value: boolean): void {
    this.listOfCurrentPageData.forEach(item => this.updateCheckedSet(item.id, value));
    this.refreshCheckedStatus();
  }

  onCurrentPageDataChange($event: readonly ItemData[]): void {
    this.listOfCurrentPageData = $event;
    this.refreshCheckedStatus();
  }

  refreshCheckedStatus(): void {
    this.checked = this.listOfCurrentPageData.every(item => this.setOfCheckedId.has(item.id));
    this.indeterminate = this.listOfCurrentPageData.some(item => this.setOfCheckedId.has(item.id)) && !this.checked;
  }

  ngOnInit(): void {
    this.originalListOfData = new Array(200).fill(0).map((_, index) => ({
      id: index,
      name: `Edward King ${index}`,
      age: Math.floor(Math.random() * (65 - 18 + 1)) + 18, // Edad aleatoria entre 18 y 65
      address: `London, Park Lane no. ${index}`
    }));
    this.listOfData = this.originalListOfData;
  }


  applyFilter(value?: string): void {
    if (value !== undefined) {
      this.searchText = value;
    }
    const filterValue = this.searchText.trim().toLowerCase();
    if (!filterValue) {
      this.listOfData = this.originalListOfData;
    } else {
      this.listOfData = this.originalListOfData.filter(item =>
        item.name.toLowerCase().includes(filterValue) ||
        item.address.toLowerCase().includes(filterValue) ||
        item.age.toString().includes(filterValue)
      );
    }
  }
}
