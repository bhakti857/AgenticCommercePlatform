export interface MasterField {
  name: string;
  label: string;
  type?: 'text' | 'number' | 'select' | 'checkbox' | 'textarea' | 'password';
  options?: { value: string | number | boolean; label: string }[];
  optionSource?: string; // API endpoint to load select options from
  required?: boolean;
  column?: boolean; // show in the List table
  form?: boolean; // editable in the Add/Edit form
  createOnly?: boolean; // only shown when creating (e.g. password)
  defaultValue?: string | number | boolean;
}

export interface MasterConfig {
  key: string;
  title: string;
  endpoint: string;
  idField: string;
  fields: MasterField[];
}

const yesNo = [
  { value: true, label: 'Yes' },
  { value: false, label: 'No' },
];

export const masterConfigs: Record<string, MasterConfig> = {
  product: {
    key: 'product',
    title: 'Product Master',
    endpoint: '/product-master',
    idField: 'productId',
    fields: [
      { name: 'productCode', label: 'Product Code', required: true, column: true },
      { name: 'productName', label: 'Product Name', required: true, column: true },
      { name: 'categoryId', label: 'Category', type: 'select', optionSource: '/category-master', required: true, column: true },
      { name: 'subCategoryId', label: 'Sub Category', type: 'select', optionSource: '/subcategory-master' },
      { name: 'unitId', label: 'Unit', type: 'select', optionSource: '/unit-master' },
      { name: 'purchasePrice', label: 'Purchase Price', type: 'number' },
      { name: 'sellingPrice', label: 'Selling Price', type: 'number', column: true },
      { name: 'gstPercent', label: 'GST %', type: 'number' },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  category: {
    key: 'category',
    title: 'Category Master',
    endpoint: '/category-master',
    idField: 'categoryId',
    fields: [
      { name: 'categoryName', label: 'Category Name', required: true, column: true },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  subcategory: {
    key: 'subcategory',
    title: 'Sub-Category Master',
    endpoint: '/subcategory-master',
    idField: 'subCategoryId',
    fields: [
      { name: 'categoryId', label: 'Category', type: 'select', optionSource: '/category-master', required: true, column: true },
      { name: 'subCategoryName', label: 'Sub-Category Name', required: true, column: true },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  unit: {
    key: 'unit',
    title: 'Unit Master',
    endpoint: '/unit-master',
    idField: 'unitId',
    fields: [
      { name: 'unitName', label: 'Unit Name', required: true, column: true },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  warehouse: {
    key: 'warehouse',
    title: 'Warehouse Master',
    endpoint: '/warehouse-master',
    idField: 'warehouseId',
    fields: [
      { name: 'warehouseName', label: 'Warehouse Name', required: true, column: true },
      { name: 'address', label: 'Address', type: 'textarea' },
      { name: 'city', label: 'City', column: true },
      { name: 'state', label: 'State' },
      { name: 'pincode', label: 'Pincode' },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  vendor: {
    key: 'vendor',
    title: 'Vendor Master',
    endpoint: '/vendor-master',
    idField: 'vendorId',
    fields: [
      { name: 'vendorName', label: 'Vendor Name', required: true, column: true },
      { name: 'email', label: 'Email' },
      { name: 'phoneNumber', label: 'Phone', column: true },
      { name: 'address', label: 'Address', type: 'textarea' },
      { name: 'city', label: 'City', column: true },
      { name: 'state', label: 'State' },
      { name: 'country', label: 'Country' },
      { name: 'pincode', label: 'Pincode' },
      { name: 'gstNumber', label: 'GST Number' },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  rawmaterial: {
    key: 'rawmaterial',
    title: 'Raw Material Master',
    endpoint: '/rawmaterial-master',
    idField: 'rawMaterialId',
    fields: [
      { name: 'rawMaterialCode', label: 'Raw Material Code', required: true, column: true },
      { name: 'rawMaterialName', label: 'Raw Material Name', required: true, column: true },
      { name: 'unitId', label: 'Unit', type: 'select', optionSource: '/unit-master' },
      { name: 'purchasePrice', label: 'Purchase Price', type: 'number' },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  department: {
    key: 'department',
    title: 'Department Master',
    endpoint: '/department-master',
    idField: 'departmentId',
    fields: [
      { name: 'departmentName', label: 'Department Name', required: true, column: true },
    ],
  },
  usertype: {
    key: 'usertype',
    title: 'User Type Master',
    endpoint: '/usertype-master',
    idField: 'userTypeId',
    fields: [
      { name: 'userTypeName', label: 'User Type Name', required: true, column: true },
    ],
  },
  customer: {
    key: 'customer',
    title: 'Customer Master',
    endpoint: '/customer-master',
    idField: 'customerId',
    fields: [
      { name: 'email', label: 'Email', required: true, column: true },
      { name: 'password', label: 'Password', type: 'password', required: true, createOnly: true },
      { name: 'firstName', label: 'First Name', required: true, column: true },
      { name: 'lastName', label: 'Last Name', required: true, column: true },
      { name: 'phoneNumber', label: 'Phone', column: true },
      { name: 'addressLine', label: 'Address', type: 'textarea' },
      { name: 'city', label: 'City' },
      { name: 'state', label: 'State' },
      { name: 'country', label: 'Country' },
      { name: 'pincode', label: 'Pincode' },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
  employee: {
    key: 'employee',
    title: 'Employee Master',
    endpoint: '/employee-master',
    idField: 'employeeId',
    fields: [
      { name: 'email', label: 'Email', required: true, column: true },
      { name: 'password', label: 'Password', type: 'password', required: true, createOnly: true },
      { name: 'firstName', label: 'First Name', required: true, column: true },
      { name: 'lastName', label: 'Last Name', required: true, column: true },
      { name: 'phoneNumber', label: 'Phone' },
      { name: 'departmentId', label: 'Department', type: 'select', optionSource: '/department-master', required: true, column: true },
      { name: 'userTypeId', label: 'User Type', type: 'select', optionSource: '/usertype-master', required: true, column: true },
      { name: 'isActive', label: 'Active', type: 'select', options: yesNo, defaultValue: true, column: true },
    ],
  },
};

export const masterOrder = [
  'product',
  'category',
  'subcategory',
  'unit',
  'warehouse',
  'vendor',
  'rawmaterial',
  'customer',
  'employee',
  'department',
  'usertype',
];