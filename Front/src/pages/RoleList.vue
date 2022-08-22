<template>
  <q-page class="q-pa-sm">
    <tables-basic :title="title" :dataobject="users" 
    :columns="columns" :createlink="createlink"
    :addbtn="addbtn" :editbtn="editbtn" :deletebtn="deletebtn"></tables-basic>
  </q-page>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import RoleDataService from '../services/RoleDataService';

const TablesBasic = defineAsyncComponent(() =>
  import('../components/tables/TableBasic.vue')
)
export default {
  
  name: "Role-List",
  data() {
    return {
      users: [],
      currentTutorial: null,
      currentIndex: -1,
      title: "Role",
      addbtn:true,
      editbtn:true,
      deletebtn:true,
      createlink:"/CreateRole",
      columns : [
  {
    name: 'id',
    required: true,
    label: '#',
    align: 'left',
    field: row => row.name,
    format: val => `${val}`,
    sortable: true
  },
    {
    name: 'name',
    required: true,
    label: 'Name',
    align: 'left',
    field: row => row.name,
    format: val => `${val}`,
    sortable: true
  },
  {name: 'description', align: 'center', label: 'Description', field: 'description', sortable: true},
  {name: 'stateCode', label: 'StateCode', field: 'stateCode', sortable: true},
  {name: 'Action', label: '', field: 'Action', sortable: false, align: 'center'}
]
    };
  },
 components: {
    TablesBasic
  },
  methods: {
    retrieveUsers() {
      var data = { };
      RoleDataService.getAll(data)
        .then(response => {
          this.users = response.data.data;
        })
        .catch(e => {
          console.log(e);
        });

    },
    refreshList() {
      this.retrieveUsers();
      this.currentUser = null;
      this.currentIndex = -1;
    },
    setActiveUser(tutorial, index) {
      this.currentUser = tutorial;
      this.currentIndex = tutorial ? index : -1;
    },
    
    searchTitle() {
      RoleDataService.findByTitle(this.title)
        .then(response => {
          this.currentUser = response.data;
          this.setActiveUser(null);
          console.log(response.data);
        })
        .catch(e => {
          console.log(e);
        });
    }
  },
  mounted() {
    this.retrieveUsers();
  }
};
</script>

<style>

</style>
