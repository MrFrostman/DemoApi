#!/bin/bash

EV=(
  'DB_NAME'
  'DB_HOST'
  )
for i in "${!EV[@]}"; do
  if [[ -v "${EV[i]}" ]]; then
    echo $(echo "${EV[i]}"="${!EV[i]}") >> ./.env
  fi
done

EV2=(
  'KEY_FILE'
  )
for i in "${!EV2[@]}"; do
  if [[ -v "${EV2[i]}" ]]; then
    echo $(echo "${!EV2[i]}") >> ./gcpcmdlineuser.json
  fi
done


EV3=(
  'KEY_FILE_SQL_ADMIN'
  )
for i in "${!EV3[@]}"; do
  if [[ -v "${EV3[i]}" ]]; then
    echo $(echo "${!EV3[i]}") >> ./gcpcmdlineuser2.json
  fi
done