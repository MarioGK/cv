<script lang='ts'>
  import { slide } from 'svelte/transition'

  import type { CurriculumProjectType } from "./Curriculum.type";
  import Toggle from "./Toggle.svelte"

  import Anchor from '../Anchor.svelte'

  import Project from './Project.svelte'

  export let projects: CurriculumProjectType[]

  let visible = true
</script>

{#if projects && projects.length > 0}
<section class="resume-section experience-section mb-5">
  <h2 on:click={() => visible = !visible} class="resume-section-title text-uppercase font-weight-bold pb-3 mb-3">
    <Anchor title="Freelance projects"/>
    <Toggle {visible}/>
    <!-- <i class="resume-award-icon fas fa-toggle-on" data-fa-transform="shrink-2"></i> -->
  </h2>
  {#if visible}
  <div class="resume-section-content" transition:slide>
    <div class="resume-timeline position-relative">

      {#each projects as project}
        <Project {project} />
      {/each}

    </div><!--//resume-timeline-->
    
  </div>
  {/if}
</section><!--//experience-section-->
{/if}