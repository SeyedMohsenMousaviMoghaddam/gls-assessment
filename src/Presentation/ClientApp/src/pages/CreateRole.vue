<template>
  <q-page class="q-pa-sm">
    <div class="row q-col-gutter-sm">
      <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
        <q-card class="card-bg text-white">
          <q-card-section class="text-h6 ">
            <div class="text-h6">{{title}} Role</div>
          </q-card-section>
          <q-card-section class="q-pa-sm">
            <q-list class="row">


              <q-item class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <q-item-section>
                  <q-select  dark color="white" v-model="role_details.name" :options="options"          
                      emit-value  map-options label="Name" />
                </q-item-section>
              </q-item>
              <q-item class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <q-item-section>
                  <q-toggle v-model="role_details.stateCode" label="StateCode" />
                </q-item-section>
              </q-item>
              <q-item class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <q-item-section>
                  <q-input dark color="white" autogrow dense v-model="role_details.description" label="Description"/>
                </q-item-section>
              </q-item>

            </q-list>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn @click="save(role_details)" class="text-capitalize bg-info text-white">{{title}} Role</q-btn>
          </q-card-actions>
        </q-card>
      </div>

    </div>
  </q-page>
</template>

<script>
import RoleDataService from '../services/RoleDataService';
import { useQuasar } from 'quasar'
import { useRoute } from 'vue-router'

export default {
  name: "RoleCU",
  setup() {
    const $q = useQuasar()
    function alarm (message) {
      $q.notify({
                    type: 'negative',
                    message: message
                })
    }
    function alert (message) {
      $q.dialog({
        dark: true,
        title: 'Alert',
        message: message
      }).onOk(() => {
        // console.log('OK')
      }).onCancel(() => {
        // console.log('Cancel')
      }).onDismiss(() => {
        // console.log('I am triggered on both OK and Cancel')
      })
    }
    return {
      alert,
      alarm
    }
  },
  data(){
    return {
      options:[{value:1,label:"Admin"},{value:0,label:"Base"}],
      role_details: {},
      title : 'Create',
      modelstate : ''
    }
  },    
  created() {
    this.getData();
  },  
  methods:{
    getData(){
      const route = useRoute()
      RoleDataService.get(route.params.id)
        .then(response => {
          this.title = 'Edit';
          this.role_details = response.data.data;
        })
        .catch(e => {})
    },    
    save(input) {
        RoleDataService.create(input)
        .then(response => {
          this.alert("Done");
        })
        .catch(e => {
          if(e.response.status == 400)
          {
            this.modelstate = e.response.data.map(function(item){return item.errorMessage;});
            this.modelstate.forEach((element) => {
              this.alarm(element);
             });
          }
        })
    }
  }
}
</script>

<style scoped>

.card-bg {
  background-color: #6c6c6e;
}
</style>