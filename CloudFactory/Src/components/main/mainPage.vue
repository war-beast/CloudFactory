<template>
	<div class="container-fluid">
		<fileRow v-for="(item, index) in fileRowCount"
				 v-bind:availableFiles="availableFiles"
				 v-bind:key="`item__${index}`" />
		<button class="btn btn-primary" type="button" v-on:click="addRow">Добавить строку</button>
		<button class="btn btn-secondary" type="button" v-on:click="deleteRow" v-if="fileRowCount > 1">Удалить строку</button>
		<button class="btn btn-secondary" type="button" v-on:click="clearCache">Очистить кеш</button>
	</div>
</template>

<script lang="ts" src="./mainPage.ts"></script>