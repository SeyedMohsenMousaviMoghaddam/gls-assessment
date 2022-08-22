<template>
  <q-page class="q-pa-sm">
    <tables-basic :title="title" :dataobject="users" 
    :columns="columns" :createlink="createlink" :infolink="infolink"
    :addbtn="addbtn" :editbtn="editbtn" :deletebtn="deletebtn" :infobtn="infobtn">
    </tables-basic>
  </q-page>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import UserDataService from '../services/UserDataService';

const TablesBasic = defineAsyncComponent(() =>
  import('../components/tables/TableBasic.vue')
)
export default {
  
  name: "User-List",
  data() {
    return {
      users: [],
      currentTutorial: null,
      currentIndex: -1,
      title: "User",
      createlink:"/CreateUser",
      infolink:"/UserLoginLogList",
      addbtn:true,
      editbtn:true,
      deletebtn:true,
      infobtn:true,
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
  {name: 'userName', align: 'center', label: 'UserName', field: 'userName', sortable: true},
  {name: 'stateCode', label: 'StateCode', field: 'stateCode', sortable: true},
  /*{name: 'calories', align: 'center', label: 'Calories', field: 'calories', sortable: true},
  {name: 'fat', label: 'Fat (g)', field: 'fat', sortable: true},
  {name: 'carbs', label: 'Carbs (g)', field: 'carbs'},
  {name: 'protein', label: 'Protein (g)', field: 'protein'},
  {name: 'sodium', label: 'Sodium (mg)', field: 'sodium'},
  {
    name: 'calcium',
    label: 'Calcium (%)',
    field: 'calcium',
    sortable: true,
    sort: (a, b) => parseInt(a, 10) - parseInt(b, 10)
  },
  {
    name: 'iron',
    label: 'Iron (%)',
    field: 'iron',
    sortable: true,
    sort: (a, b) => parseInt(a, 10) - parseInt(b, 10)
  },*/
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
      UserDataService.getAll(data)
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
      UserDataService.findByTitle(this.title)
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
