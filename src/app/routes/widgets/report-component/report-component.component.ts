import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Component } from '@angular/core';
import { NzTreeFlatDataSource, NzTreeFlattener, NzTreeViewModule } from 'ng-zorro-antd/tree-view';

import { FormsModule } from '@angular/forms';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { auditTime, map } from 'rxjs/operators';

import { NzHighlightPipe } from 'ng-zorro-antd/core/highlight';
import { NzNoAnimationDirective } from 'ng-zorro-antd/core/no-animation';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';

//TODO: Hacer una limpieza completa y acomodamiento completo de todo el codigo TS y LESS que tenga lineas de codigo basuca que no sirven para nada.

interface TreeNode {
  name: string;
  children?: TreeNode[];
}

const TREE_DATA: TreeNode[] = [
  {
    name: '0-0',
    children: [{ name: '0-0-0' }, { name: '0-0-1' }, { name: '0-0-2' }]
  },
  {
    name: '0-1',
    children: [
      {
        name: '0-1-0',
        children: [{ name: '0-1-0-0' }, { name: '0-1-0-1' }]
      },
      {
        name: '0-1-1',
        children: [{ name: '0-1-1-0' }, { name: '0-1-1-1' }]
      }
    ]
  }
];

interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

class FilteredTreeResult {
  constructor(
    public treeData: TreeNode[],
    public needsToExpanded: TreeNode[] = []
  ) { }
}

/**
 * From https://stackoverflow.com/a/45290208/6851836
 */
function filterTreeData(data: TreeNode[], value: string): FilteredTreeResult {
  const needsToExpanded = new Set<TreeNode>();
  const _filter = (node: TreeNode, result: TreeNode[]): TreeNode[] => {
    if (node.name.search(value) !== -1) {
      result.push(node);
      return result;
    }
    if (Array.isArray(node.children)) {
      const nodes = node.children.reduce((a, b) => _filter(b, a), [] as TreeNode[]);
      if (nodes.length) {
        const parentNode = { ...node, children: nodes };
        needsToExpanded.add(parentNode);
        result.push(parentNode);
      }
    }
    return result;
  };
  const treeData = data.reduce((a, b) => _filter(b, a), [] as TreeNode[]);
  return new FilteredTreeResult(treeData, [...needsToExpanded]);
}

interface FoodNode {
  name: string;
  disabled?: boolean;
  children?: FoodNode[];
}
/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  disabled: boolean;
}
@Component({
  selector: 'app-report-component',
  templateUrl: './report-component.component.html',
  styleUrls: ['./report-component.component.less'],
})


export class ReportComponentComponent {
  flatNodeMap = new Map<FlatNode, TreeNode>();
  nestedNodeMap = new Map<TreeNode, FlatNode>();
  expandedNodes: TreeNode[] = [];
  searchValue = '';
  originData$ = new BehaviorSubject(TREE_DATA);
  searchValue$ = new BehaviorSubject<string>('');

  transformer = (node: TreeNode, level: number): FlatNode => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode =
      existingNode && existingNode.name === node.name
        ? existingNode
        : {
          expandable: !!node.children && node.children.length > 0,
          name: node.name,
          level
        };
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  };

  treeControl = new FlatTreeControl<FlatNode, TreeNode>(
    node => node.level,
    node => node.expandable,
    {
      trackBy: flatNode => this.flatNodeMap.get(flatNode)!
    }
  );

  treeFlattener = new NzTreeFlattener<TreeNode, FlatNode, TreeNode>(
    this.transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );

  dataSource = new NzTreeFlatDataSource(this.treeControl, this.treeFlattener);

  filteredData$ = combineLatest([
    this.originData$,
    this.searchValue$.pipe(
      auditTime(300),
      map(value => (this.searchValue = value))
    )
  ]).pipe(map(([data, value]) => (value ? filterTreeData(data, value) : new FilteredTreeResult(data))));

  constructor() {
    this.filteredData$.subscribe(result => {
      this.dataSource.setData(result.treeData);

      debugger;

      const hasSearchValue = !!this.searchValue;
      if (hasSearchValue) {
        if (this.expandedNodes.length === 0) {
          this.expandedNodes = this.treeControl.expansionModel.selected;
          this.treeControl.expansionModel.clear();
        }
        this.treeControl.expansionModel.select(...result.needsToExpanded);
      } else {
        if (this.expandedNodes.length) {
          this.treeControl.expansionModel.clear();
          this.treeControl.expansionModel.select(...this.expandedNodes);
          this.expandedNodes = [];
        }
      }
    });
  }

  hasChild = (_: number, node: FlatNode): boolean => node.expandable;




}
