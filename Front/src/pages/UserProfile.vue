<template>
  <q-page class="q-pa-sm">
    <div class="row q-col-gutter-sm">

      <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
        <q-card class="card-bg text-white">
          <q-card-section class="text-h6 q-pa-sm">
            <div class="text-h6">Change Password</div>
          </q-card-section>
          <q-card-section class="q-pa-sm row">
            <q-item class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
              <q-item-section>
                Current Password
              </q-item-section>
            </q-item>
            <q-item class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
              <q-item-section>
                <q-input type="password" dark dense outlined color="white" round
                         v-model="password_dict.oldPassword"
                         label="Current Password"/>
              </q-item-section>
            </q-item>
            <q-item class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
              <q-item-section>
                New Password
              </q-item-section>
            </q-item>
            <q-item class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
              <q-item-section>
                <q-input type="password" dark dense outlined color="white" round v-model="password_dict.newPassword"
                         label="New Password"/>
              </q-item-section>
            </q-item>
            <q-item class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
              <q-item-section>
                Confirm New Password
              </q-item-section>
            </q-item>
            <q-item class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
              <q-item-section>
                <q-input type="password" dark dense outlined round color="white"
                         v-model="password_dict.confirmNewPassword"
                         label="Confirm New Password"/>
              </q-item-section>
            </q-item>
          </q-card-section>
          <q-card-actions align="right">
            <q-btn @click="save(password_dict)" class="text-capitalize bg-info text-white">Change Password</q-btn>
          </q-card-actions>

        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script>
import UserDataService from '../services/UserDataService';
import { useQuasar } from 'quasar'
import { useRoute } from 'vue-router'

export default {
  name: "UserProfile",
  props: {
    id:0
  },
  setup() {
    const route = useRoute()
    function save(password_dict) {
      if(route.params.id)
      {
        password_dict.userId = route.params.id;
        UserDataService.changePasswordAllUser(password_dict)
        .then(response => {
          alert('successfully.');
        })
        .catch(e => {
          console.log(e);
        });
      }
      else
      {
        UserDataService.changePassword(password_dict)
        .then(response => {
          alert('successfully.');
        })
        .catch(e => {
          console.log(e);
        });
      }
    }
    const $q = useQuasar()
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
      password_dict: {
        oldPassword :'',
        newPassword :'',
        confirmNewPassword:'' ,
        userId : 0
      },
      alert,
      save
    }
  },
  data(){
   
    return {
    }
  }
}
</script>

<style scoped>

.card-bg {
  background-color: #353536;
}
</style>
