<template>
  <q-page class="q-pa-sm">
    <tables-basic :title="title" :dataobject="users" :columns="columns"></tables-basic>
  </q-page>
</template>

<script>
import { defineAsyncComponent } from 'vue'
import UserDataService from '../services/UserDataService';

const TablesBasic = defineAsyncComponent(() =>
  import('../components/tables/TableBasic.vue')
)
export default {
  
  name: "UserLoginLog-List",
  data() {
    return {
      id: this.$route.params.id,
      users: [],
      currentTutorial: null,
      currentIndex: -1,
      title: "UserLoginLog",
      columns : [
   {
    name: 'id',
    required: true,
    label: '#',
    align: 'left',
    field: row => row.id,
    format: val => `${val}`,
    sortable: true
  },
    {
    name: 'userId',
    required: true,
    label: 'UserId',
    align: 'left',
    field: row => row.userId,
    format: val => `${val}`,
    sortable: true
  },
  {name: 'userName', align: 'center', label: 'UserName', field: 'userName', sortable: true},
  {name: 'loginDate', label: 'LoginDate', field: 'loginDate', sortable: true},
  {name: 'Action', label: '', field: 'Action', sortable: false, align: 'center'}
]
    };
  },
 components: {
    TablesBasic
  },
  methods: {
    retrieveUsers() {
      UserDataService.getAllLog(this.id)
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

  },
  mounted() {
    this.retrieveUsers();
  }
};
</script>

<style>

</style>
