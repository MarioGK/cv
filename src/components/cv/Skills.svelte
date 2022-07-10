<script type='ts'>
  import { slide } from 'svelte/transition'

  import { toList } from "../../utils/string";

  import Anchor from '../Anchor.svelte'

  import type { CurriculumOtherSkillsType, CurriculumSkillAreaType } from "./Curriculum.type";
  import Toggle from "./Toggle.svelte"

  export let skillAreas: CurriculumSkillAreaType[]
  export let otherSkills: CurriculumOtherSkillsType

  let visible = true
</script>

<section class="resume-section skills-section mb-5">
  <h2 on:click={() => visible = !visible} class="resume-section-title text-uppercase font-weight-bold pb-3 mb-3">
    <Anchor title="Skills &amp; Tools" />
    <Toggle {visible}/>
    <!-- <i class="resume-award-icon fas fa-toggle-on" data-fa-transform="shrink-2"></i> -->
  </h2>

  {#if visible}
    <div class="resume-section-content" transition:slide>
      <div class="resume-skill-item">
        {#each skillAreas as skillArea}
        <h4 class="resume-skills-cat font-weight-bold">{ skillArea.title }</h4>
        <ul class="list-unstyled mb-4">
          {#each skillArea.skills as skill}
          <li class="mb-2">
            <div class="resume-skill-name">{ skill.title }</div>
            <div class="progress resume-progress">
              <div class="progress-bar theme-progress-bar-dark" role="progressbar" style="width: { skill.experience }%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
            </div>
          </li>
          {/each}
        </ul>
        {/each}
      </div><!--//resume-skill-item-->

      {#if otherSkills && otherSkills.length > 0}
      <div class="resume-skill-item">
        <h4 class="resume-skills-cat font-weight-bold">Others</h4>
        <ul class="list-inline">
          {#each toList(otherSkills) as skill }
            <li class="list-inline-item"><span class="badge badge-light">{ skill }</span></li>
          {/each}
        </ul>
      </div><!--//resume-skill-item-->
      {/if}
    </div><!--resume-section-content-->
  {/if}
</section><!--//skills-section-->